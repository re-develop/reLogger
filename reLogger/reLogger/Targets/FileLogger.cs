using reLogger.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;

namespace reLogger.Targets
{
	public class FileLogger : ILoggerTarget
	{
		public string Name => "FileLogger";

		public Guid Id { get; private set; }

		public HashSet<ILoggerCategory> Whitelist { get; private set; } = new HashSet<ILoggerCategory>();

		private FileStream LogStream = null;
		private Task LogTask = null;
		private string LogDirectory { get; set; } = string.Empty;
		private string FileNameFormat { get; set; } = string.Empty;

		private DateTime StreamCreationTime { get; set; }
		private ManualResetEvent manualResetEvent = new ManualResetEvent(false);

		private ConcurrentQueue<(string, ILoggerCategory)> LogQueue { get; set; } = new ConcurrentQueue<(string, ILoggerCategory)>();

		public FileLogger(string logDirectory = "logs", string nameFormat = "dd.MM.yyyy")
		{
			Id = Guid.NewGuid();

			if (!Directory.Exists(logDirectory))
				Directory.CreateDirectory(logDirectory);

			FileNameFormat = nameFormat;
			LogDirectory = logDirectory;

			LogTask = Task.Run(() =>
			{
				while (true)
				{
					if (!LogQueue.IsEmpty)
					{
						while (LogQueue.TryDequeue(out (string, ILoggerCategory) currentItem))
						{
							if (Whitelist.Contains(currentItem.Item2))
							{
								byte[] encodedMessage = Encoding.UTF8.GetBytes(currentItem.Item1 + "\r\n");
								FileStream currentLogStream = GetLoggerStream();

								currentLogStream.Write(encodedMessage, 0, encodedMessage.Length);
								currentLogStream.Flush();
							}
						}
					}

					manualResetEvent.WaitOne(5000);
				}
			});

			LogTask.Start();
		}

		public void Dispose()
		{
			LogStream?.Dispose();
			LogTask?.Dispose();
		}

		public void Write(string message, ILoggerCategory loggerCategory)
		{
			LogQueue.Enqueue((message, loggerCategory));
			manualResetEvent.Set();
		}

		private FileStream GetLoggerStream()
		{
			DateTime currentTime = DateTime.UtcNow;

			if ((StreamCreationTime - currentTime) > TimeSpan.FromDays(1))
			{
				LogStream.Close();
				LogStream.Dispose();

				File.Move(Path.Combine(LogDirectory, "lastest.log"),
					Path.Combine(LogDirectory, currentTime.ToString(FileNameFormat)));

				LogStream = new FileStream(Path.Combine(LogDirectory, "latest.log"),
					FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);

				StreamCreationTime = currentTime;
			}

			return LogStream;
		}
	}
}
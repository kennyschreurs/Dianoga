﻿using System.IO;
using Sitecore.Pipelines;
using Sitecore.Resources.Media;

namespace Dianoga.Processors
{
	public class ProcessorArgs : PipelineArgs
	{
		public MediaStream InputStream { get; }

		public MediaOptions MediaOptions { get; }

		public string Extension { get; set; }

		public Stream ResultStream { get; set; }

		public bool IsOptimized { get; set; }

		public ProcessorArgsStatistics Statistics { get; }

		public ProcessorArgs(MediaStream inputStream)
		{
			InputStream = inputStream;
			Statistics = new ProcessorArgsStatistics(this);
		}

		public ProcessorArgs(MediaStream inputStream, MediaOptions options) : this(inputStream)
		{
			MediaOptions = options;
		}

		public class ProcessorArgsStatistics
		{
			private readonly ProcessorArgs _args;

			internal ProcessorArgsStatistics(ProcessorArgs args)
			{
				_args = args;
				SizeBefore = _args.InputStream.Length;
			}

			public long SizeBefore { get; }
			public long SizeAfter => _args.ResultStream?.Length ?? SizeBefore;
			public float PercentageSaved => 1 - ((SizeAfter / (float)SizeBefore));
			public long BytesSaved => SizeBefore - SizeAfter;
		}
	}
}

namespace uTinyRipper.BundleFiles
{
	public sealed class BundleFileEntry : FileEntry, IBundleReadable
	{
		public static bool HasBlobIndex(BundleGeneration generation)
		{
			return generation >= BundleGeneration.BF_530_x;
		}

		public void Read(BundleReader reader)
		{
			if (HasBlobIndex(reader.Generation))
			{
				Offset = reader.ReadInt64();
				Size = reader.ReadInt64();
				BlobIndex = reader.ReadInt32();
				NameOrigin = reader.ReadStringZeroTerm();
			}
			else
			{
				NameOrigin = reader.ReadStringZeroTerm();
				Offset = reader.ReadInt32();
				Size = reader.ReadInt32();
			}
			Name = FilenameUtils.FixFileIdentifier(NameOrigin);
		}

		public int BlobIndex { get; private set; }
	}
}

/* Reference:
https://github.com/ephtracy/voxel-model/blob/master/MagicaVoxel-file-format-vox.txt
https://github.com/ephtracy/voxel-model/blob/master/MagicaVoxel-file-format-vox-extension.txt
*/

using System;
using System.IO;
using System.Linq;
using VoxReader.Extensions;
using VoxReader.Interfaces;
using UnityEngine;

namespace VoxReader
{
    /// <summary>
    /// Used to read data from .vox files.
    /// </summary>
    public static class VoxReader
    {
        /// <summary>
        /// Reads the file at the provided path.
        /// </summary>
        public static IVoxFile Read(string filePath, bool useBSA)
        {
            byte[] data = useBSA ? BetterStreamingAssets.ReadAllBytes(filePath) : File.ReadAllBytes(filePath);

            return Read(data);
        }
        
        /// <summary>
        /// Reads the data from the provided byte array.
        /// </summary>
        public static IVoxFile Read(byte[] data)
        {
            int versionNumber = BitConverter.ToInt32(data, 4);

            IChunk mainChunk = ChunkFactory.Parse(data.GetRange(8));

            var palette = new Palette(mainChunk.GetChild<IPaletteChunk>().Colors);

            var models = Helper.ExtractModels(mainChunk, palette).ToArray();

            return new VoxFile(versionNumber, models, palette, mainChunk.Children);
        }
    }
}
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using lilToon;


namespace lilToon
{
    /// <summary>
    /// Startup method provider.
    /// </summary>
    internal static class Startup
    {
        /// <summary>
        /// Buffer size of streams.
        /// </summary>
        private const int BufferSize = 8192;

        /// <summary>
        /// A method called at Unity startup.
        /// </summary>
        [InitializeOnLoadMethod]
        private static void OnStartup()
        {
            AssetDatabase.importPackageCompleted += Startup_ImportPackageCompleted;
            UpdateVersionDefFile();
        }

        /// <summary>
        /// Update definition file of version value of lilToon, lil_current_version_value.hlsl.
        /// </summary>
        private static void UpdateVersionDefFile()
        {
            var guidShaderDir = TemplateAllInspector.GuidShaderDir;
            var dstDirPath = AssetDatabase.GUIDToAssetPath(guidShaderDir);
            if (dstDirPath == "")
            {
                Debug.LogWarning("Cannot find file or directory corresponding to GUID: " + guidShaderDir);
                return;
            }
            if (!Directory.Exists(dstDirPath))
            {
                Debug.LogWarningFormat("Directory not found: {0} ({1})", dstDirPath, guidShaderDir);
                return;
            }

            using (var ms = new MemoryStream(BufferSize))
            {
                WriteVersionFileBytes(ms);
                var buffer = ms.GetBuffer();
                var length = (int)ms.Length;
                var dstFilePath = Path.Combine(dstDirPath, "lil_current_version_value.hlsl");
                if (CompareFileBytes(dstFilePath, buffer, 0, length))
                {
                    return;
                }
                using (var fs = new FileStream(dstFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    fs.Write(buffer, 0, length);
                }

                Debug.Log($"Update {dstFilePath}");
            }
        }

        /// <summary>
        /// Write version file content to <see cref="s"/>.
        /// </summary>
        /// <param name="s">Destination stream.</param>
        private static void WriteVersionFileBytes(Stream s)
        {
            using (var writer = new StreamWriter(s, Encoding.ASCII, BufferSize, true))
            {
                writer.Write("#define LIL_CURRENT_VERSION_VALUE {0}\n", lilConstants.currentVersionValue);
            }
        }

        /// <summary>
        /// Compare file content with specified byte sequence.
        /// </summary>
        /// <param name="filePath">Target file path.</param>
        /// <param name="contentData">File content data to compare.</param>
        /// <param name="offset">Offset of <paramref name="contentData"/>,</param>
        /// <param name="length">Length of <paramref name="contentData"/>.</param>
        /// <returns>True if file content is same to <see cref="contentData"/>, otherwise false.</returns>
        private static bool CompareFileBytes(string filePath, byte[] contentData, int offset, int length)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }
            if (new FileInfo(filePath).Length != length)
            {
                return false;
            }

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var buffer = new byte[Math.Min(BufferSize, length)];
                int nRead;
                while ((nRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    if (!CompareMemory(buffer, 0, contentData, offset, nRead))
                    {
                        return false;
                    }
                    offset += nRead;
                }
            }

            return true;
        }

        /// <summary>
        /// Compare two byte data.
        /// </summary>
        /// <param name="data1">First byte data array.</param>
        /// <param name="offset1">Offset of first byte data array.</param>
        /// <param name="data2">Second byte data array.</param>
        /// <param name="offset2">Offset of second byte data array.</param>
        /// <param name="length">Data length of <paramref name="data1"/> and <paramref name="data2"/>.</param>
        /// <returns>True if two byte data is same, otherwise false.</returns>
        private static bool CompareMemory(byte[] data1, int offset1, byte[] data2, int offset2, int length)
        {
            for (int i = 0; i < length; i++)
            {
                if (data1[i] != data2[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// A callback method for <see cref="AssetDatabase.importPackageCompleted"/>.
        /// </summary>
        /// <param name="packageName">Imported package name.</param>
        private static void Startup_ImportPackageCompleted(string packageName)
        {
            if (!packageName.StartsWith("lilToon"))
            {
                return;
            }
            UpdateVersionDefFile();
        }
    }
}

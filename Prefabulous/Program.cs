using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MiloLib;
using MiloLib.Assets;
using MiloLib.Assets.Band;
using MiloLib.Utils;

class Program
{
    static int Main(string[] args)
    {
        if (args.Length < 2)
        {
            ShowHelp();
            return 1;
        }

        string scenePath = args[0];
        string assetPath = args[1];

        if (!File.Exists(scenePath))
        {
            Console.WriteLine("Error: Scene file not found: " + scenePath);
            return 2;
        }

        if (!File.Exists(assetPath))
        {
            Console.WriteLine("Error: BandCharDesc file not found: " + assetPath);
            return 3;
        }

        try
        {
            MiloFile scene = new MiloFile(scenePath);

            // Always target the root directory
            DirectoryMeta root = scene.dirMeta;

            string assetName = Path.GetFileName(assetPath); // or Path.GetFileNameWithoutExtension(assetPath)

            // Skip if an entry with the same name already exists in the root
            bool exists = root.entries.Any(e => string.Equals(e.name, assetName, StringComparison.OrdinalIgnoreCase));
            if (exists)
            {
                Console.WriteLine($"Skip: '{assetName}' already exists in the root directory.");
                return 0;
            }

            ImportBandCharDesc(root, assetPath, assetName);
            Console.WriteLine($"Added '{assetName}' (BandCharDesc) to root.");

            // Save scene (preserve compression/endian; PS3 special-case uncompressed)
            var compression = scene.compressionType;
            if (scene.dirMeta.platform == DirectoryMeta.Platform.PS3)
            {
                compression = MiloFile.Type.Uncompressed;
            }
            scene.Save(null, compression, 2064U, Endian.LittleEndian, scene.endian);

            Console.WriteLine("Saved scene: " + scene.filePath);
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            if (Debugger.IsAttached) Console.WriteLine(ex);
            return 4;
        }
    }

    static void ShowHelp()
    {
        Console.WriteLine("Prefabulous - Import a prefab (BandCharDesc) asset into the ROOT of a Milo scene");
        Console.WriteLine("Usage:");
        Console.WriteLine("  Prefabulous <scenePath> <bandCharDescPath>");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  Prefabulous scene.milo_xbox prefab_custom_name");
        Console.WriteLine("  Prefabulous scene.milo_ps3 prefab_custom_name");
    }

    // Import a BandCharDesc with the asset name equal to the input file name
    static void ImportBandCharDesc(DirectoryMeta dir, string path, string name)
    {
        byte[] fileBytes = File.ReadAllBytes(path);

        var entry = DirectoryMeta.Entry.CreateDirtyAssetFromBytes(
            "BandCharDesc",
            name,
            fileBytes.ToList());

        dir.entries.Add(entry);

        using (EndianReader reader = new EndianReader(new MemoryStream(fileBytes), Endian.BigEndian))
        {
            entry.obj = new BandCharDesc().Read(reader, false, dir, entry);
        }
    }
}

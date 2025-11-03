# Prefabulous

This is a barebones implementation of ihatecompvir's [MiloEditor](https://github.com/ihatecompvir/MiloEditor) to accomplish one task only, a cli dependency that can add custom prefabs to the rb3 prefabs file. Meant to be used in conjunction with the Rock Band 3 Deluxe build system for ease of use.

This script was written by ai, its just a copy paste of ImportAsset from ImMilo gui.


## Usage
`Prefabulous <scenePath> <bandCharDescPath>`
`Prefabulous scene.milo_xbox prefab_custom_name`
`Prefabulous scene.milo_ps3 prefab_custom_name`

## Credits

- [ihatecompvir](https://github.com/ihatecompvir/) - for MiloEditor and the example MiloUtil

- [Sulfrix](https://github.com/Sulfrix) - for creating the cross-platform ImMilo GUI to replace the Windows-only WinForms UI

- [PikminGuts92 (Cisco)](https://github.com/PikminGuts92) - for creating the 010 Editor templates, Mackiloha, and Grim that have all been a gigantic help in understanding Milo scenes

- [RB3 Decomp team and contributors](https://github.com/DarkRTA/rb3/tree/master) - for decompiling the game into clean, human readable source code, making it significantly easier to write this

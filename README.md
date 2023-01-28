# TFC editor

### A GUI & commandline tool for texture dumping and editing Unreal Engine 3 Texture File Cache (.tfc) files.

# About
To open the GUI run the program with no arguments.

To use the CLI run the program with arguments.

### Dumping
TFC files do not contain all information to conrrectly reconstruct an image from raw bytes and thus this program infers the dds attributes, this has limitations. The infering alogrithm works properly on `NxN` textures where `N=2^k` and the format is DXT1 or DXT5 (which is in my experience almost all files). Files not matching this will still export with no data lost, although they might end up looking wrong.

The texture ids will probably NOT be exactly the same as from other tfc exporters as this tool indexes all textures in the file, not just the ones it is able to dump. Hence this texture id will end up slightly larger than other tfc exporters.

Textures are (by default) placed at `./out/dump/<tfc name>_<id>_<properties>.dds`

### Editing Textures
TFC Editor can only replace textures with a texture of the same dimensions and format. Additionally it must able to lzo compress the replacement texture to the same size or smaller than the texture it's trying to replace.
Thankfully UE uses kinda weak compression by default and tfc editor uses super agressive compression meaning most reasonable replacements work. If it fails try reducing noise/complexity in the replacement texture.

Edited tfc are (by default) placed at `./out/edited/<tfc name>.tfc`

Replacing textures on already edited tfc files is no recommended. If you want to replace multiple textures do so at the same time using a clean tfc file.

# How to use

### examples
`tfc_editor myfile.tfc --dump *` dump all textures

`tfc_editor myfile.tfc --dump 5` dump texture 5

`tfc_editor myfile.tfc --dump 0-100` dump texture 0 to 100

`tfc_editor myfile.tfc --dump 1,2,100-102` dump texture 1,2 and 100 to 102

`tfc_editor myfile.tfc --dump ./mydumplist.txt` reads `mydumplist.txt`, replaces newlines with `,` and uses that as an argument

`tfc_editor myfile.tfc --replace 5:mytextures/tex1override.dds` replace texture of id 5 with file located at `mytextures/tex1override.dds` 

`tfc_editor myfile.tfc --replace 5:tex5.dds,6:tex6.dds` replace texture 5 with `tex5.dds` and 6 `tex5.dds`

`tfc_editor myfile.tfc --dump ./myreplacelist.txt` reads `myreplacelist.txt`, replaces newlines with ´,´ and uses that as an the argument

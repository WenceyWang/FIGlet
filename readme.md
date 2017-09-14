# FiGlet.Net

[![GitHub issues](https://img.shields.io/github/issues/WenceyWang/FIGlet.Net.svg?style=flat-square)](https://github.com/WenceyWang/FIGlet.Net/issues)
[![GitHub forks](https://img.shields.io/github/forks/WenceyWang/FIGlet.Net.svg?style=flat-square)](https://github.com/WenceyWang/FIGlet.Net/network)
[![GitHub stars](https://img.shields.io/github/stars/WenceyWang/FIGlet.Net.svg?style=flat-square)](https://github.com/WenceyWang/FIGlet.Net/stargazers)
[![GitHub license](https://img.shields.io/badge/license-AGPLv3-blue.svg?style=flat-square)](https://github.com/WenceyWang/FIGlet.Net/blob/master/LICENSE)
[![NuGet Status](http://img.shields.io/nuget/v/Figlet.Net.svg?style=flat-square)](https://www.nuget.org/packages/FIGlet.Net)

.Net standard 2.0 Lib used to create ASCII art.

Use .fig font.

Written in C#.

Previous https://github.com/auriou/FIGlet but I have totally rewritten it.

## How to Use

```csharp
var text = new WenceyWang.FIGlet.AsciiArt("Text");
text.ToString(); //Get string
var result = text.Result; //Get every Single line
```

Load your font

```csharp
var font = new WenceyWang.FIGlet.FIGletFont(fontStream);
var text = new WenceyWang.FIGlet.AsciiArt("Text", font: font, width: WenceyWang.FIGlet.CharacterWidth.Full);
```

## Donate

Bitcoin:  132p657FgxmmL9foJY3KfmSpVfe3WHxZZL

ETH:  0x4A498B0B62cBDA10a526972cA2ffEff65Aba7Da0

LTC:  LdLYidVEQkXWcFzTujaYmihvggVLZLNuyx
# Control sonos speakers from dotnet

[![Latest version][badge_nuget]][link_nuget]
[![Github Issues][badge_issues]][link_issues]
[![Star on Github][badge_repo_stars]][link_repo]

Always wanted to control sonos speakers right from your dotnet application? I've created this library for you to do just that.

**Warning** This library is far from complete at the moment, it's just an experiment for now check [features](#features) for more details.

## Control sonos in other languages

[![Sonos net][badge_sonos-csharp]][link_repo]
[![Sonos typescript][badge_sonos-typescript]][link_sonos-typescript]
[![Sonos api documentation][badge_sonos-docs]][link_sonos-docs]
[![Sonos2mqtt][badge_sonos-mqtt]][link_sonos-mqtt]

## Show your support

[![Support me on Github][badge_sponsor]][link_sponsor]
[![Follow on Twitter][badge_twitter]][link_twitter]
[![Check my blog][badge_blog]][link_blog]

## Features

Currently this library is not at version one, these are the planned features that need to be build before this can be called a version one.

- [X] All sonos services generated from service discovery
- [X] Event subscriptions
- [X] Dynamic metadata generation based on [these docs](https://svrooij.io/sonos-api-docs/metadata.html)
- [ ] SonosManager class that keeps track of speaker groups
- [ ] Device discovery, though it hardly works

## CodeTour available

This project uses [CodeTour](https://marketplace.visualstudio.com/items?itemName=vsls-contrib.codetour) in [Visual Studio Code](https://code.visualstudio.com/) to describe how stuff works. If you want to contribute to this library, I suggest you to take a look at the code tour just to get started.

## Contribute

We welcome all contributions to this project, to get started be sure to checkout the [CodeTour](#codetour-available) which will explain how some files get generated.

If you see a file with the `.gen.cs` suffix it means that it is generated. Manual changes to these files will not be accepted because they will get lost upon next generation.

This library is [licensed](./LICENSE.md) under **GPL v3** and all contributions are considered to be publishable under that same license.

[badge_blog]: https://img.shields.io/badge/blog-svrooij.io-blue?style=for-the-badge
[badge_issues]: https://img.shields.io/github/issues/svrooij/sonos-net?style=for-the-badge
[badge_nuget]: https://img.shields.io/nuget/v/Sonos.Base?style=for-the-badge
[badge_sonos-csharp]: https://img.shields.io/badge/sonos-C%23-blue?style=for-the-badge
[badge_sonos-docs]: https://img.shields.io/badge/sonos-documentation-blue?style=for-the-badge
[badge_sonos-mqtt]: https://img.shields.io/badge/sonos-mqtt-blue?style=for-the-badge
[badge_sonos-typescript]: https://img.shields.io/badge/sonos-typescript-blue?style=for-the-badge
[badge_sponsor]: https://img.shields.io/github/sponsors/svrooij?logo=github&style=for-the-badge
[badge_repo_stars]: https://img.shields.io/github/stars/svrooij/sonos-net?logo=github&style=for-the-badge
[badge_twitter]: https://img.shields.io/twitter/follow/svrooij?logo=twitter&style=for-the-badge

[link_blog]: https://svrooij.io
[link_issues]: https://github.com/svrooij/sonos-api-docs/issues
[link_nuget]: https://www.nuget.org/packages/Sonos.Base/
[link_sonos-docs]: https://svrooij.io/sonos-api-docs
[link_sonos-mqtt]: https://svrooij.io/sonos2mqtt
[link_sonos-typescript]: https://svrooij.io/node-sonos-ts
[link_sponsor]: https://github.com/sponsors/svrooij
[link_repo]: https://github.com/svrooij/sonos-net
[link_twitter]: https://twitter.com/svrooij

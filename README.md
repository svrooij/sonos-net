# Control sonos speakers from dotnet

[![Follow on Twitter][badge_twitter]][link_twitter]
[![Star on Github][badge_repo_stars]][link_repo]
[![Support me on Github][badge_sponsor]][link_sponsor]
[![Check my blog][badge_blog]][link_blog]

Always wanted to control sonos speakers right from your dotnet application? I've created this library for you to do just that.

**Warning** This library is far from complete at the moment, it's just an experiment for now.

## Developer

A lot in this library is generated by the [sonos generator](https://github.com/svrooij/sonos-api-docs/tree/main/generator/sonos-docs). You can use the following script to regenerate the pre-generated models.

```bash
# Install the generator (once)
npm install -g @svrooij/sonos-docs

# Combine the different sources to one simple file (once, unless service definition changed)
sonos-docs combine

# Generate library (windows)
sonos-docs generate .\src\sonos-net-template\ .\src\

# Generate library (linux/mac)
sonos-docs generate ./src/sonos-net-template/ ./src/

# Fix formatting
# dotnet tool install -g dotnet-format
dotnet-format
```


[badge_blog]: https://img.shields.io/badge/blog-svrooij.io-blue?style=for-the-badge
[badge_repo_stars]: https://img.shields.io/github/stars/svrooij/sonos-net?logo=github&style=for-the-badge
[badge_sponsor]: https://img.shields.io/github/sponsors/svrooij?logo=github&style=for-the-badge
[badge_twitter]: https://img.shields.io/twitter/follow/svrooij?logo=twitter&style=for-the-badge

[link_blog]: https://svrooij.io
[link_repo]: https://github.com/svrooij/sonos-net
[link_sponsor]: https://github.com/sponsors/svrooij
[link_twitter]: https://twitter.com/svrooij
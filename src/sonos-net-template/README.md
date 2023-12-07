# sonos-net template

## Folder content

| File | Description | Remarks |
|:-----|:------------|:--------|
| [Service](./Service.hbs) | Single service | Strong type service template |
| [SonosDevice](./SonosDevice.hbs) | All services partial | Strong type service template |

## Generator

See the generator documentation [here](https://github.com/svrooij/sonos-api-docs/tree/main/generator/sonos-docs).

## Regenerate services

```Shell
# Once to generate the template json locally
npx @svrooij/sonos-docs combine --docsUrl=https://sonos.svrooij.io/documentation.json

npx @svrooij/sonos-docs generate ./src/sonos-net-template/ ./src/
```

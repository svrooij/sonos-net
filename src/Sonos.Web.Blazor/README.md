# Sonos.Web.Blazor

This project will serve as an interface to the Sonos Web API using Blazor.

## Generate client code

```powershell
kiota generate -l csharp -d ..\Sonos.Web\openapi.v1.json -c SonosWebClient -n Sonos.Web.Blazor.Client -o ./Client/Generated --clean-output
```

### Old specs

```powershell
kiota generate -l csharp -d ..\Sonos.Web\obj\Sonos.Web.json -c SonosWebClient -n Sonos.Web.Blazor.Client -o ./Client/Generated --clean-output
```

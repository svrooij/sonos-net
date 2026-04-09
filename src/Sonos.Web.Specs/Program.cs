using Sonos.Web.Specs;

var apiFactory = new SonosWebFactory();

var client = apiFactory.CreateClient();

var specs = await client.GetAsync("/openapi/v1.json");
specs.EnsureSuccessStatusCode();

var specsData = await specs.Content.ReadAsByteArrayAsync();

var filename = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "Sonos.Web", "openapi.v1.json"));
await File.WriteAllBytesAsync(filename, specsData);

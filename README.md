# URLSessions

Implementation of URL Session similar to Swift. It provides an API to make HTTP requests or download files easily.

## Classes

```csharp
URL("https://example.example.com");
URLRequest(url, URLRequest.Method.GET, timeout: 15);
URLSession.shared.DataTask<List<ToDOs>>(request);
```

## Usage

Make HTTP Requests:

```csharp
var url = new URL("https://example.example.com/get");
var getRequest = new URLRequest(url, URLRequest.Method.GET, timeout: 15);
var (response, statusCode) = await URLSession.shared.DataTask<List<ToDOs>>(request);

```

Add your model as a HTTP body easily:

```csharp
struct Model
{
	public int id;
	public string name;
	public bool active;
}

var url = new URL("https://example.example.com/post");
var request = new URLRequest(url, URLRequest.Method.POST);
var model = new Model(1, "Jafar", true);

request.SetHTTPBody(model);

var (response, statusCode) = await URLSession.shared.DataTask<Model>(request);
```

Set HTTP Headers:

```csharp
var url = new URL("https://example.example.com/post");
var request = new URLRequest(url, URLRequest.Method.POST);

request.SetValue("application/json", "Accept");
request.SetValue("123456789", "Authorization");

```

Download Files:

```csharp
var url = new URL("https://example.example.com/file");
var dest = "C:/users/user/desktop/file.jpg"
var statusCode = await URLSession.shared.Download(url, dest);

```

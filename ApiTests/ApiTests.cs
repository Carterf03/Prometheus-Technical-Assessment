using NUnit.Framework;
using RestSharp;
using System.Net;

[TestFixture]
public class ApiTests
{
    private RestClient client;

    [SetUp]
    public void Setup()
    {
        client = new RestClient("https://jsonplaceholder.typicode.com/");
    }

    [Test]
    public void get_post()
    {
        var request = new RestRequest("posts/1", Method.Get);
        var response = client.Execute<Post>(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data.id, Is.EqualTo(1));
    }

    [Test]
    public void get_posts()
    {
        var request = new RestRequest("posts/", Method.Get);
        var response = client.Execute<Post>(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data, Is.Not.Null);
    }

    [Test]
    public void get_nonexisting_post()
    {
        var request = new RestRequest("posts/0", Method.Get);
        var response = client.Execute<Post>(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public void create_post()
    {
        var request = new RestRequest("posts/", Method.Post);
        request.AddJsonBody(new { userId = 1, title = "New Title", body = "This is a new post." });
        var response = client.Execute<Post>(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data.userId, Is.EqualTo(1));
        Assert.That(response.Data.title, Is.EqualTo("New Title"));
        Assert.That(response.Data.body, Is.EqualTo("This is a new post."));
    }

    [Test]
    public void update_post()
    {
        var request = new RestRequest("posts/1", Method.Put);
        request.AddJsonBody(new { userId = 3, title = "Updated Title", body = "I am updating this post." });
        var response = client.Execute<Post>(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data.userId, Is.EqualTo(3));
        Assert.That(response.Data.title, Is.EqualTo("Updated Title"));
        Assert.That(response.Data.body, Is.EqualTo("I am updating this post."));
    }

    [Test]
    public void delete_post()
    {
        var request = new RestRequest("posts/1", Method.Delete);
        var response = client.Execute<Post>(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data.userId, Is.EqualTo(0));
        Assert.That(response.Data.title, Is.Null);
        Assert.That(response.Data.body, Is.Null);
    }
}
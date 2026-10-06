using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;


namespace MyBGList.OpenApi;

public class ProblemsDemoDescriptionTransformer : IOpenApiOperationTransformer
{
    private static readonly Dictionary<string, string> Descriptions = new()
    {
        ["429"] = """
            **rate-limited** — `https://api.mybglist.com/scalar#rate-limited`

            Returned when the caller exceeds the allowed request rate.

            | Field | Type | Description |
            |---|---|---|
            | `retryAfterSeconds` | `int` | How long to wait before retrying |
            | `limit` | `int` | Max requests allowed per window |
            | `windowSeconds` | `int` | Length of the rate-limit window |
            """,
        ["409"] = """
            **stale-resource** — `https://api.mybglist.com/scalar#stale-resource`

            Returned when a client's update conflicts with a newer version already
            saved on the server (optimistic concurrency).

            | Field | Type | Description |
            |---|---|---|
            | `clientVersion` | `int` | Version number the client had |
            | `serverVersion` | `int` | Current version number on the server |
            """,
    };

    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        foreach (var (statusCode, description) in Descriptions)
        {
            if (operation.Responses.TryGetValue(statusCode, out var response))
            {
                response.Description = description;
            }
        }

        return Task.CompletedTask;
    }
}

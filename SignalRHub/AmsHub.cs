using Microsoft.AspNetCore.SignalR;

public class AmsHub : Hub
{
    public async Task JoinRoleGroup(string roleId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"Role_{roleId}");
    }
}


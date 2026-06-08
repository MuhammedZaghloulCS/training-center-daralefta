using Microsoft.AspNetCore.SignalR;

namespace API.Hubs.Notifications
{
    public class NotificationHub :Hub
    {

        public async Task AddUsersToTrainingProgramGroup(string trainingProgramName,
            int trainingProgramId,
            List<string> userIds)
        {
            var groupName =
                $"{trainingProgramName}_{trainingProgramId}";

            foreach (var userId in userIds)
            {
                await Groups.AddToGroupAsync(
                    userId,
                    groupName);
            }

            await Clients.Group(groupName)
                .SendAsync(
                    "UsersAddedToGroup",
                    groupName);
        }
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(
                    Context.ConnectionId,
                    userId);
            }
            await base.OnConnectedAsync();
        }
    }

}

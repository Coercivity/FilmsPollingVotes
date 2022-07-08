﻿using LobbyMVC.Dtos;
using LobbyMVC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LobbyMVC.Hubs
{
    public class LobbyManager
    {
        public Dictionary<string, List<SignalRMessageObject>> HubCache { get; set; } = new();

        public List<LobbyUser> Users { get; } = new();

        public void ConnectUser(string userName, string connectionId)
        {
            var userAlreadyExists = GetConnectedUserByName(userName);
            if (userAlreadyExists != null)
            {
                userAlreadyExists.AppendConnection(connectionId);
                return;
            }

            var user = new LobbyUser(userName);
            user.AppendConnection(connectionId);
            Users.Add(user);
        }



        public bool DisconnectUser(string connectionId)
        {
            var userExists = GetConnectedUserById(connectionId);
            if (userExists == null)
            {
                return false;
            }

            if (!userExists.Connections.Any())
            {
                return false;
            }

            var connectionExists = userExists.Connections.Select(x => x.ConnectionId).First().Equals(connectionId);
            if (!connectionExists)
            {
                return false;
            }

            if (userExists.Connections.Count() == 1)
            {
                Users.Remove(userExists);
                return true;
            }

            userExists.RemoveConnection(connectionId);
            return false;
        }



        public bool CheckIfKinopoiskIdExistsInGroup(string message, string groupId, out string id)
        {
            if (KinopoiskLinkParser.IsLink(message))
            {
                id = KinopoiskLinkParser.GetFilmIdFromLink(message);
            }
            else
            {
                id = message;
            }

            foreach (var item in HubCache[groupId])
            {
                if (item.Film.KinopoiskId.Equals(int.Parse(id)))
                {
                    return true;
                }
            }
            return false;
        }

        private LobbyUser? GetConnectedUserById(string connectionId) =>
            Users
                .FirstOrDefault(x => x.Connections.Select(c => c.ConnectionId)
                .Contains(connectionId));



        private LobbyUser? GetConnectedUserByName(string userName) =>
           Users
               .FirstOrDefault(x => string.Equals(
                   x.UserName,
                   userName,
                   StringComparison.CurrentCultureIgnoreCase));
    }
}

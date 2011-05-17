<%@ WebService Language="C#" Class="WebService" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.SessionState;

public static class Chat
{
    public static Dictionary<string, List<Message>> rooms;
    public static List<User> users;

    static Chat()
    {
        users = new List<User>();
        rooms = new Dictionary<string, List<Message>>();
        rooms.Add("default", new List<Message>());
    }
}

public class Message
{
    public string Username;
    public string Msg;
    public DateTime time;
}

public class User
{
    public string nick;
    public string sessionId;
    public DateTime lastFetch;
}

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public bool AuthUser(string username)
    {
        Session["username"] = username;

        bool response = true;
        if (Chat.users.Find(u => u.sessionId == Session.SessionID) == null)
        {
            Chat.users.Add(new User() { lastFetch = DateTime.Now, sessionId = Session.SessionID, nick = username });
        }
        return response;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public List<Message> GetChat(string chatroom)
    {
        User user = Chat.users.Find(u => u.sessionId == Session.SessionID);
        DateTime lastFetch = user.lastFetch;
        user.lastFetch = DateTime.Now;
        return Chat.rooms[chatroom].FindAll(e => lastFetch < e.time && e.Username != user.nick);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public bool PostMessage(string message, string chatroom)
    {
        bool response = false;
        User user = Chat.users.Find(u => u.sessionId == Session.SessionID);

        if (user != null)
        {
            if (Chat.rooms.Keys.First(k => k == chatroom) == null)
            {
                Chat.rooms.Add(chatroom, new List<Message>());
            }

            Chat.rooms[chatroom].Add(new Message() { Username = user.nick, Msg = message, time = DateTime.Now });
            response = true;
        }

        return response;
    }
}

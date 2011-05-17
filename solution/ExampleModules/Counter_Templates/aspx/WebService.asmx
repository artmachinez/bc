<%@ WebService Language="C#" Class="WebService" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.SessionState;

public static class WebSiteInfo
{
    public static List<Visitor> visitors;

    static WebSiteInfo()
    {
        visitors = new List<Visitor>();
    }
}

public class Visitor
{
    public string sessionId;
    public string IP;
    public DateTime lastVisit;
}

public class Data
{
    public int currentVisitors;
    public int totalVisitors;
    public string requestIP;
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
    public Data GetData()
    {
        Data data = new Data();
        Session["username"] = "username";
        
        Visitor user = WebSiteInfo.visitors.Find(u => u.sessionId == Session.SessionID);
        if(user == null)
        {
          user = new Visitor();
          user.IP = HttpContext.Current.Request.UserHostAddress;
          user.sessionId = Session.SessionID;
          WebSiteInfo.visitors.Add(user);
          
        }
        user.lastVisit = DateTime.Now;
        
        data.totalVisitors =  WebSiteInfo.visitors.Count();
        data.requestIP = HttpContext.Current.Request.UserHostAddress;
        data.currentVisitors = WebSiteInfo.visitors.FindAll(u => u.lastVisit > (DateTime.Now - new TimeSpan(0, 0, 6))).Count();
        
        return data;
    }
}

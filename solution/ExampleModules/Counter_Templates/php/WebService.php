<?php
session_start();
// classes init
class WebSiteInfo
{
    public $users;

    function __construct()
    {
        $users = array();
    }
}

class Data
{
    public $currentVisitors;
    public $totalVisitors;
    public $requestIP;
}

class Visitor
{
    public $sessionId;
    public $IP;
    public $lastVisit;
}

// get chat class instance
$s = file_get_contents('websiteinfo.txt');
if($s == "")
{
  $chat = new WebSiteInfo();
}
else
{
  $websiteinfo = unserialize($s);
}

if($_GET['action'] == "GetData")
{
    $data = new Data();

    @$user = $chat->users[session_id()];
    if($user == null)
    {
        $user = new Visitor();
        $user->IP = 123;
        $user->sessionId = session_id();
        $websiteinfo->users[session_id()] = $user;
    }
    
    $user->lastVisit = time();
    
    $data->totalVisitors = count($websiteinfo->users);
    $data->requestIP = $_SERVER['REMOTE_ADDR'];
    $data->currentVisitors = 0;
    
    foreach($websiteinfo->users as $visitor)
    {
       if($visitor->lastVisit > (time() - 6)) 
          $data->currentVisitors++;
    }
    echo json_encode($data); 
}

file_put_contents("websiteinfo.txt", serialize($websiteinfo));
?>
<?php
session_start();
// classes init
class Chat
{
    public $rooms;
    public $users;

    function __construct()
    {
        $users = array();
        $rooms = array();
        $rooms["default"] = array();
    }
}

class Message
{
    public $Username;
    public $Msg;
    public $time;
}

class User
{
    public $nick;
    public $sessionId;
    public $lastFetch;
}

// get chat class instance
$s = file_get_contents('chatsession.txt');
if($s == "")
{
  $chat = new Chat();
}
else
{
  $chat = unserialize($s);
}

if($_GET['action'] == "AuthUser")
{
    $incdata = file_get_contents('php://input');
    $data = json_decode($incdata);
    $user = new User();
    $user->nick = $data->username;
    $user->sessionId = session_id();
    $user->lastFetch = time();
    $chat->users[$user->sessionId] = $user;
    echo json_encode(TRUE);
}

if($_GET['action'] == "GetChat")
{
    $user = $chat->users[session_id()];
  
    $filtered_array = array();
    foreach($chat->rooms["default"] as $message)
    {
       if($message->time > $user->lastFetch && $message->Username != $user->nick) 
        $filtered_array[] = $message; 
    }
    
    $user->lastFetch = time();
    echo json_encode($filtered_array); 
}

if($_GET['action'] == "getAll")
{   
    echo "<pre>";
    var_dump($chat); 
    echo "</pre>";
}

if($_GET['action'] == "PostMessage")
{
    $incdata = file_get_contents('php://input');
    $data = json_decode($incdata);
    $user = $chat->users[session_id()];
    $msg = new Message();
    $msg->Username = $user->nick;
    $msg->time = time();
    $msg->Msg = $data->message;
    
    $chat->rooms["default"][] = $msg;    

    echo json_encode(TRUE); 
}

file_put_contents("chatsession.txt", serialize($chat));
?>
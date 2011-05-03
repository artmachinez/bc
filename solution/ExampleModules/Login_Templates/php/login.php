<?php

session_start();

if(isset($_POST))
{
  if($_POST['getuser'] == 'true')
  {
    if(isset($_SESSION['username']))
      echo $_SESSION['username'];
  }
  elseif($_POST['logout'] == 'true')
  {
      if(isset($_SESSION['username']))
        $_SESSION['username'] = '';
        
      echo "success";
  }
  else{
    if(isset($_POST['username']) && isset($_POST['password']))
    {
      $_SESSION['username'] = $_POST['username'];
      echo "logged as " . $_SESSION['username'];
    }
  }


}


?>
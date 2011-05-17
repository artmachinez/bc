<style type="text/css">
  #chat { margin: 20px;padding:10px; border: 1px solid #DDDDDD;  width:{{_setup.setup_width}};  }
  #chat div#chatWindow { text-align: left; overflow: scroll; background-color: {{_setup.setup_bgcolor}}; width:100%; height: {{_setup.setup_height}}; }
  #chat textarea#chatInput { width:99%; background-color: {{_setup.setup_bgcolor}}; border: 1px solid #111111;margin-top:10px; }
</style>
<div id='div_{{_setup.id}}'>
  <div id="chat">
    <div id="chatWindow"></div>
    <textarea id="chatInput" type="text"></textarea>
  </div>
</div>
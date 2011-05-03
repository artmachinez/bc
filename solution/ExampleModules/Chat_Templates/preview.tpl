<style type="text/css">
h2_{{_setup.id}}                              { color: {{_setup.setup_headercolor}}; font: 30px Helvetica, Sans-Serif; margin: 0 0 10px 0; }
#page-wrap_{{_setup.id}}                      { width: {{_setup.setup_width}}; margin: 30px auto; position: relative; }

#chat-wrap_{{_setup.id}}                      { border: 1px solid #eee; margin: 0 0 15px 0; }
#chat-area_{{_setup.id}}                      { height: 300px; overflow: auto; border: 1px solid #666; padding: 20px; background: {{_setup.setup_bgcolor}}; }
#chat-area_{{_setup.id}} span                 { color: white; background: #333; padding: 4px 8px; -moz-border-radius: 5px; -webkit-border-radius: 8px; margin: 0 5px 0 0; }
#chat-area_{{_setup.id}} p                    { padding: 8px 0; border-bottom: 1px solid #ccc; }

#name-area_{{_setup.id}}                      { position: absolute; top: 12px; right: 0; color: white; font: bold 12px "Lucida Grande", Sans-Serif; text-align: right; }
#name-area_{{_setup.id}} span                 { color: #fa9f00; }

#send-message-area_{{_setup.id}} p            { float: left; color: white; padding-top: 27px; font-size: 14px; }
#sendie_{{_setup.id}}                         { border: 3px solid #999; width: {{_setup.setup_width}}; padding: 10px; font: 12px "Lucida Grande", Sans-Serif; float: right; }
</style>
<div id="page-wrap_{{_setup.id}}">

  <h2>{{_setup.setup_headertext}}</h2>

  <p id="name-area_{{_setup.id}}"></p>

  <div id="chat-wrap_{{_setup.id}}">
    <div id="chat-area_{{_setup.id}}"></div>
  </div>

  <form id="send-message-area_{{_setup.id}}">
    <p>Your message: </p>
    <textarea id="sendie_{{_setup.id}}" maxlength = '100' ></textarea>
  </form>

</div>
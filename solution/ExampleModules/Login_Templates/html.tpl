<style type="text/css">
  #div_{{_setup.id}} {background: {{_setup.setup_background}}; width: 130px;}
  #form_{{_setup.id}} input {color: {{_setup.setup_inputcolor}}; border: 1px solid black}
  #form_{{_setup.id}} label { width: 4em; float: left; text-align: right; margin-right: 0.5em; display: block }
  #accept_{{_setup.id}} { margin-left: 4.5em; }
</style>

<div id='div_{{_setup.id}}'>
  <script src="modules/{{_setup.id}}/caller.js"></script>

  <form id="form_{{_setup.id}}">
    <p>
      <label for="username_{{_setup.id}}">name</label>
      <input id='username_{{_setup.id}}' name='name' type='text' />
    </p>

    <p>
      <label for='password_{{_setup.id}}'>pass</label>
      <input id='password_{{_setup.id}}' name='password' type='password' />
    </p>
    <p>
      <input id='accept_{{_setup.id}}' value='login' type='submit' />
    </p>
  </form>
  <div id='answer_{{_setup.id}}' >   </div>
  <a id='logout_{{_setup.id}}'>logout</a>
</div>
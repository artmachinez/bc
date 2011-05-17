<style type="text/css">
  #counter { padding:10px; border: 1px solid #DDDDDD;  width:200px;  }
  #counter.div { margin: 5px; background-color: #EEEEEE;  border: 1px solid #111111; }
</style>
<div id="counter">
  <div>
    Current users : <span>24</span>
  </div>
  <div>
    Total Users : <span>52</span>
  </div>
  {% if _setup.setup_showip != '0' %}
  <div>
    Your IP : <span>123.123.123.123</span>
  </div>
  {% endif %}
</div>
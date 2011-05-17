<script type="text/javascript" src="modules/{{_setup.id}}/counter.js"></script>
<link rel="Stylesheet" href="modules/{{_setup.id}}/default.css">

  <script type="text/javascript">
    $(document).ready(function () {
    $('.counter').counter();
    });
  </script>

  <div class="counter">
    <div class="item">
      Current users : <span class="currentVisBox"></span>
    </div>
    <div class="item">
      Total Users : <span class="totalVisBox"></span>
    </div>
    {% if _setup.setup_showip != '0' %}
    <div class="item">
      Your IP : <span class="IPBox"></span>
    </div>
    {% endif %}
  </div>
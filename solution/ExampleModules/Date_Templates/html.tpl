<script>
  var currentTime = new Date();
  var month = currentTime.getMonth() + 1;
  var day = currentTime.getDate();
  var year = currentTime.getFullYear();
  var hours = currentTime.getHours()
  var minutes = currentTime.getMinutes()

  if (minutes < 10)
    minutes = "0" + minutes

  document.write(month + "/" + day + "/" + year + " - <b>" + hours + ":" + minutes + " " + "</b>")
</script>
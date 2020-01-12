'use strict'
let id = $("option:selected").attr('name');
let data;
$.get("{{ route('showAd') }}", data,
  function (data, textStatus, jqXHR) {
    data.forEach(element => {
      console.log(element.title);
    });
  },
  "JSON"
);
function showItem(e) {
let id = $("option:selected").attr('name');
let data;
$.get("{{ route('showAd') }}/" + id, data,
  function (data, textStatus, jqXHR) {
    console.log(data[0].title);
  },
  "JSON"
);
}
let items = new Array();

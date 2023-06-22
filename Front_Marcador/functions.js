const Api_GetMarcadores_URL = "https://localhost:7112/api/Marcadores"

var platform = new H.service.Platform({
  'apikey': '{tSA_z9s-3x3IxPQdIeM8wY0OvydIkuUS5-zawNOkmUQXZvRdUroiK8NV7C0rD6acqGq1dzpFsKcbG7PsIckrtA}'
});

var defaultLayers = platform.createDefaultLayers();

var map = new H.Map(document.getElementById('map'), defaultLayers.vector.normal.map, {
  center: {lat:-31.4135, lng:-64.18105},
  zoom: 13,
  pixelRatio: window.devicePixelRatio || 1
});

var behavior = new H.mapevents.Behavior(new H.mapevents.MapEvents(map));
var ui = H.ui.UI.createDefault(map, defaultLayers);

function agregarMarcadores(coordinates) {
  var marker = new H.map.Marker(coordinates);
  map.addObject(marker);
}


function cargarMarcadores() {
  
  fetch(Api_GetMarcadores_URL)
    .then((respuesta) => respuesta.json())
    .then((respuesta) => {
      if (respuesta.statusCode !== 200) {
        alert('Error');       
        return;
      } else {
        respuesta.listMarcadores.forEach(marcador => {
          var marker = new H.map.Marker({
            lat: marcador.latitud,
            lng: marcador.longitud
          });        
          map.addObject(marker);
         
        });
      }
    });
}

function onMarkerClick(event) {
  var marker = event.target;
  var data = marker.getData();

  var infoWindow = new H.ui.InfoBubble(marker.getGeometry(), {
    content: data.info 
  });
  ui.addBubble(infoWindow);
}
function initializeMap() {
  cargarMarcadores();
}  
document.addEventListener('DOMContentLoaded',initializeMap);






window.addEventListener('resize', () => map.getViewPort().resize());


window.onload = function () {
  agregarMarcadores(map);
}
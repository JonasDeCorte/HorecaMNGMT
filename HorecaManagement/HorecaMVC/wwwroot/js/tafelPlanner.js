// create a wrapper around native canvas element (with id="drawing-area")
const canvas = new fabric.Canvas('drawing-area', {
    height: 600,
    width: 600,
});
// zoom and pan

canvas.on('mouse:wheel', function (opt) {
    var delta = opt.e.deltaY;
    var zoom = canvas.getZoom();
    zoom *= 0.999 ** delta;
    if (zoom > 20) zoom = 20;
    if (zoom < 0.01) zoom = 0.01;
    canvas.zoomToPoint({ x: opt.e.offsetX, y: opt.e.offsetY }, zoom);
    opt.e.preventDefault();
    opt.e.stopPropagation();
});
canvas.on('mouse:down', function (opt) {
    var evt = opt.e;
    if (evt.altKey === true) {
        this.isDragging = true;
        this.selection = false;
        this.lastPosX = evt.clientX;
        this.lastPosY = evt.clientY;
    }
});
canvas.on('mouse:move', function (opt) {
    if (this.isDragging) {
        var e = opt.e;
        var vpt = this.viewportTransform;
        vpt[4] += e.clientX - this.lastPosX;
        vpt[5] += e.clientY - this.lastPosY;
        this.requestRenderAll();
        this.lastPosX = e.clientX;
        this.lastPosY = e.clientY;
    }
});
canvas.on('mouse:up', function (opt) {
    // on mouse up we want to recalculate new interaction
    // for all objects, so we call setViewportTransform
    this.setViewportTransform(this.viewportTransform);
    this.isDragging = false;
    this.selection = true;
});

document.body.onclick = function (e) {
    e = e || window.event;
    //   console.log("event: " + e);
    var target = e.target || e.srcElement;
    //   console.log("target: " + target);
    var isShape = target.nodeName === 'IMG' && (' ' + target.className + ' ').indexOf(' shape ') > -1;
    if (isShape) {
        $(document).ready(function () {
            e.preventDefault();
            $(document).on("click", "#AddModal", OpenModal);
            $("#btnSubmit").click(function () {
                var seats = document.getElementById("seats-text").value;
                var name = document.getElementById("table-name").value;
                var form = document.getElementById("AddTableForm");
                console.log(seats);
                console.log(name);
                if ((seats != null && seats !== '') && (name != null && name !== '')) {
                    if (!isNaN(seats) && seats > 0) {
                        fabric.Image.fromURL(target.src, function (image) {
                            canvas.add(image.set({
                                Id: getRandomIntInclusive(1000, 9999999),
                                Name: name,
                                Seats: seats,
                            }));
                            DrawTableWithChairs(image, canvas, seats);
                            const jsondata = JSON.stringify(canvas.toDatalessJSON(['Id', 'Name', 'Seats']));
                            console.log(jsondata);
                        });
                    }
                }
                form.reset();
                $('#AddModal').modal('hide'); // does't work for some reason??
            })
        });
      
       
      
    }
};

document.onkeydown = function (e) {
    e = e || window.event;
    var activeObject = canvas.getActiveObject();
    if (activeObject) {
        var distance = e.shiftKey ? 10 : 1;
        switch (e.keyCode) {
            case 8: /* backspace */
                canvas.remove(activeObject);
                console.log("removing: " + activeObject);
                break;
            case 37: /* left */
                activeObject.set('left', activeObject.get('left') - distance).setCoords();
                console.log("move : " + activeObject + "to the left");
                canvas.renderAll();
                break;
            case 39: /* right */
                activeObject.set('left', activeObject.get('left') + distance).setCoords();
                console.log("move : " + activeObject + "to the right");
                canvas.renderAll();
                break;
            case 40: /* down */
                activeObject.set('top', activeObject.get('top') + distance).setCoords();
                console.log("move : " + activeObject + "down");
                canvas.renderAll();
                break;
            case 38: /* up */
                activeObject.set('top', activeObject.get('top') - distance).setCoords();
                console.log("move : " + activeObject + "up");
                canvas.renderAll();
                break;
        }
        return false;
    }
};

document.getElementById('clear-canvas').onclick = function () {
    canvas.clear();
    return false;
};
document.getElementById('toImage').onclick = function () {
    var data = canvas.toDataURL({ multiplier: 2, format: 'png' });
    console.log(data);
    downloadDataUrl(data);

    return false;
};

async function downloadDataUrl(dataURL) {
    const blob = await fetch(dataURL).then(r => r.blob());
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.download = "FloorPlan.png"
    a.href = url;
    a.click();
    URL.revokeObjectURL(url);
    a.remove();
}

$("#ToJson").click(function () {
    var floorplanCanvas = canvas.toDatalessJSON(['Id', 'Name', 'Seats']);
    var floorplanId = $(this).data("id");
    $.ajax({
        type: "post",
        dataType: "application/json",
        cache: false,
        url: "/Table/CreateTables/" + floorplanId,
        data: JSON.stringify(floorplanCanvas),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            window.location.href = result;
        }
    });
});

$("#FromJson").click(function () {
    var data = $(this).data("json");
    var JSONData = JSON.stringify(data);
    console.log(JSONData);
    canvas.loadFromJSON(JSONData, canvasJSONCallBack, function (o, object) {
        canvas.setActiveObject(object);
    });
});

$(document).ready(function () {
    var element = document.getElementById("FromJson");
    var data = $(element).data("json");
    var JSONData = JSON.stringify(data);
    console.log(JSONData);
    canvas.loadFromJSON(JSONData, canvasJSONCallBack, function (o, object) {
        object.set('selectable', false);
        canvas.setActiveObject(object);
    });
});

function DrawTableWithChairs(image, canvas, seats) {
    canvas.centerObjectH(image).centerObjectV(image);
    image.setCoords();
    canvas.renderAll();
    var coords = image.aCoords;

    var center = image.getCenterPoint();
    console.log("center: " + image.getCenterPoint());
    var chairs = seats;
    var cx = center.x, cy = center.y;
    //var radius = Math.sqrt(Math.pow(coords.tr.y - center.y, 2) + Math.pow(coords.tr.x - coords.tl.x / 2, 2));
    var radius = 90;
    console.log("Radius :" + radius);
    var degree_step = Math.PI * 2 / chairs;
    console.log("cx: " + cx);
    console.log("cy: " + cy);

    var size = {
        width: window.innerWidth || document.body.clientWidth,
        height: window.innerHeight || document.body.clientHeight
    }
    console.log("size :" + size);
    //if (size.width > 1200) {
    //radius = radius - 145;
    //}
    //else {
    //    radius = radius - 110;
    //}

    for (var count = 0; count < chairs; count++) {
        console.log("angle: " + count * degree_step);
        var x = cx + radius * Math.cos(count * degree_step);
        var y = cy + radius * Math.sin(count * degree_step);
        console.log("x: " + x);
        console.log("y: " + y);

        x = x - 25;
        y = y - 25;

        var rect = new fabric.Rect({
            top: y,
            left: x,
            fill: 'black',
            width: 50,
            height: 50,
            excludeFromExport: true
        });
        canvas.add(rect);
    }
    canvas.renderAll();
}

function canvasJSONCallBack() {
    canvas.renderAll();
    canvas.calcOffset();
    var length = canvas.getObjects().length;
    console.log(length);
    for (var i = 0; i < length; i++) {
        var image = canvas.item(i);
        console.log("image : " + image + "id: " + i);
        image.calcACoords();
        console.log(image.aCoords);



        var coords = image.aCoords
        var center = image.getCenterPoint();
        console.log("center: " + image.getCenterPoint());
        var chairs = image.seats;
        var cx = center.x, cy = center.y;
        //var radius = Math.sqrt(Math.pow(coords.tr.y - center.y, 2) + Math.pow(coords.tr.x - coords.tl.x / 2, 2));
        var radius = 90;
        console.log("Radius :" + radius);
        var degree_step = Math.PI * 2 / chairs;



        console.log("cx: " + cx);
        console.log("cy: " + cy);



        //var size = {
        // width: window.innerWidth || document.body.clientWidth,
        // height: window.innerHeight || document.body.clientHeight
        //}
        //console.log(size);
        //radius = radius - 145;



        for (var count = 0; count < chairs; count++) {
            console.log("angle: " + count * degree_step);
            var x = cx + radius * Math.cos(count * degree_step);
            var y = cy + radius * Math.sin(count * degree_step);
            console.log("x: " + x);
            console.log("y: " + y);
            x = x - 25;
            y = y - 25;



            var rect = new fabric.Rect({
                top: y,
                left: x,
                fill: 'black',
                width: 50,
                height: 50,
                excludeFromExport: true
            });
            canvas.add(rect);
        }
        canvas.renderAll();
    }
    console.log("post: " + canvas.getObjects());
}

function getRandomIntInclusive(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1) + min); //The maximum is inclusive and the minimum is inclusive
}

resizeWindow = function () {
    const outerCanvasContainer = document.getElementById('drawing-container');

    const ratio = canvas.getWidth() / canvas.getHeight();
    const containerWidth = outerCanvasContainer.clientWidth;
    const scale = containerWidth / canvas.getWidth();
    const zoom = canvas.getZoom() * scale;

    canvas.setDimensions({ width: containerWidth, height: containerWidth / ratio });
    canvas.setViewportTransform([zoom, 0, 0, zoom, 0, 0]);
}




function OpenModal() {
    $("#addModal").modal();
}
// Example starter JavaScript for disabling form submissions if there are invalid fieldss
(function () {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })
})()
window.onload = resizeWindow;
window.onresize = resizeWindow;
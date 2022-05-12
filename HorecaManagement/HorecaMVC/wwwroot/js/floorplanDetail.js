// create a wrapper around native canvas element (with id="drawing-area")
const canvas = new fabric.Canvas('drawing-area', {
    height: 600,
    width: 600,
});
// zoom and pan
canvas.selection = false;

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
    if (opt.target) {
        if (opt.target.id) {
            console.log('ID:', opt.target.id);
            var element = document.getElementById("FromJson");
            var floorplanId = $(element).data("id");
            window.location.href = "/Table/Detail/" + opt.target.id + "/" + floorplanId;
        }
    } else {
        console.log('not a target');
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
            rect.set('selectable', false);
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

window.onload = resizeWindow;
window.onresize = resizeWindow;
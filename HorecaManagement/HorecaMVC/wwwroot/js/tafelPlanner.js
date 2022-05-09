// create a wrapper around native canvas element (with id="drawing-area")
const canvas = new fabric.Canvas('drawing-area', {
    width: 600,
    height: 600,
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
    console.log(target.className);
    console.log(target.nodeName);
    console.log(isShape);
    if (isShape) {
        var name = prompt("fix name ", "name");
        var seats = prompt("fix aantal stoelen  ", 4);
        var c = document.getElementById('drawing-area');

        fabric.Image.fromURL(target.src, function (image) {
            canvas.add(image.set({
                Id: getRandomIntInclusive(1000, 9999999),
                Name: name,
                Seats: seats,
            }));

            image.scaleToWidth(200);
            canvas.centerObjectH(image).centerObjectV(image);
            image.setCoords();
            canvas.renderAll();

            //var ctx = c.getContext("2d");
            //var angle = 0;
            //var chairs = 6;
            //var cx = canvas.width >> 1, cy = canvas.height >> 1, radius = (canvas.height >> 1) - 10;
            //var step = Math.PI * 2 / chairs;

            //for (var count = 0; count < chairs; count++) {
            //    var x = cx + radius * Math.cos(angle);
            //    var y = cy + radius * Math.sin(angle);
            //    ctx.rect(x - 5, y - 5, 10, 10);
            //    angle += step;
            //}
            //ctx.stroke();
            canvas.renderAll();
            const jsondata = JSON.stringify(canvas.toDatalessJSON(['Id', 'Name', 'Seats']));
            console.log(jsondata);
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

document.getElementById('ToJson').onclick = function () {
    console.log(JSON.stringify(canvas.toDatalessJSON(['Id', 'Name', 'Seats'])));
    return false;
};

document.getElementById('FromJson').onclick = function () {
    var JSONData = prompt("json");
    canvas.loadFromJSON(JSONData, canvasJSONCallBack, function (o, object) {
        canvas.setActiveObject(object);
    });
};

function canvasJSONCallBack() {
    canvas.renderAll();
    canvas.calcOffset();
}
function getRandomIntInclusive(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1) + min); //The maximum is inclusive and the minimum is inclusive
}
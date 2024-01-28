import qrcode from "qrcode-generator"

function makeQr(options) {
    let qr = qrcode(0, options.errorCorrection);
    qr.addData(options.data);
    qr.make();
    return qr;
}

function calculateCellSize(options, qr) {
    let cellSize = options.cellSize;
    if (cellSize == null && options.targetSize != null) {
        let modules = qr.getModuleCount();
        cellSize = Math.max(1, (options.targetSize - options.margin * 2) / modules);
    }
    return cellSize;
}

export function getImage(optionsJson) {
    let options = JSON.parse(optionsJson);

    let qr = makeQr(options);

    let cellSize = calculateCellSize(options, qr);

    return qr.createImgTag(cellSize, options.margin, options.altText);
}

export function getSvg(optionsJson) {
    let options = JSON.parse(optionsJson);

    let qr = makeQr(options);

    let cellSize = calculateCellSize(options, qr);

    return qr.createSvgTag({
        cellSize: cellSize,
        margin: options.margin,
        scalable: options.scalable
    });
}

export function getTable(optionsJson) {
    let options = JSON.parse(optionsJson);

    let qr = makeQr(options);

    let cellSize = calculateCellSize(options, qr);

    return qr.createTableTag(cellSize, options.margin);
}
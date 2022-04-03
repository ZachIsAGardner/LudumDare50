const fs = require('fs');
const path = require('path');

const assetsPath = `${__dirname}/../sprites/tilesets`;
const contentPath = `${__dirname}/../tilesets`;

// ---

function parseDirectory(path, output) {
    output = output || [];
    var items = fs.readdirSync(path);

    items.forEach(item => {
        var obj = { relativePath: `${path.replace(`${__dirname}/../`, "")}/${item}`, path: `${path}/${item}`, name: item };
        output.push(obj);
        if (fs.lstatSync(obj.path).isDirectory()) {
            output = parseDirectory(obj.path, output);
        }
    });

    return output;
}

function sleep(ms) {
    return new Promise((resolve) => {
        setTimeout(resolve, ms);
    });
}

console.log("Writing to Content.mgcb ...");

var resourceFiles = parseDirectory(`${assetsPath}`);

var template = `[gd_resource type="TileSet" load_steps=2 format=2]\n\n`;

var files = [];

var id = 1;

resourceFiles.filter(r => r.name.endsWith(".png")).forEach(f => {
    var file = { name: f.name.split(".png").join(""), contents: template };
    file.contents += `[ext_resource path="res://${f.relativePath}" type="Texture" id=1]\n\n[resource]`;
    var rows = 4;
    var columns = 4;
    var tileSize = 16;
    var i = 0;
    for (let r = 0; r < rows; r++) {
        for (let c = 0; c < columns; c++) {
            file.contents += `
${i}/name = "${file.name}_${i}"
${i}/texture = ExtResource( 1 )
${i}/tex_offset = Vector2( 0, 0 )
${i}/modulate = Color( 1, 1, 1, 1 )
${i}/region = Rect2( ${c * tileSize}, ${r * tileSize}, ${tileSize}, ${tileSize} )
${i}/tile_mode = 0
${i}/occluder_offset = Vector2( 0, 0 )
${i}/navigation_offset = Vector2( 0, 0 )
${i}/shape_offset = Vector2( 0, 0 )
${i}/shape_transform = Transform2D( 1, 0, 0, 1, 0, 0 )
${i}/shape_one_way = false
${i}/shape_one_way_margin = 0.0
${i}/shapes = [  ]
${i}/z_index = 0`;

            i++;
        }
    }

    files.push(file);
});

// Done!

async function write() {
    // fs.readdir(contentPath, (err, files) => {
    //     if (err) throw err;

    //     for (const file of files) {
    //         fs.unlink(path.join(contentPath, file), err => {
    //             if (err) throw err;
    //         });
    //     }
    // });

    // await sleep(1000);

    files.forEach(f => {
        fs.writeFileSync(`${contentPath}/${f.name}.tres`, f.contents);
    });
}

write();

console.log("Successfully created tilesets!");
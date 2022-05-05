// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*
Author: Haoran Geng
Partner:   None
Date:      11 / 24 / 2021
Course:    CS 4540, University of Utah, School of Computing
Copyright: CS 4540 and Haoran Geng - This work may not be copied for use in Academic Coursework.


File Contents:
JavaScript for PS8
*/


    var app = new PIXI.Application({backgroundColor: 0x808080 });//gray
    var b_width = 100;
    var b_height = 30;
    var border = 2;
    var mouse_down = false;
    /**
     *  Create PIXI stage
     */
    function setup_pixi_stage(width, height) {

        app.renderer.resize(width, height);
    $("#canvas_div").append(app.view);


    }

    /**
     *  Create a Checker Object
     */
    class Box extends PIXI.Graphics {

        constructor(pos_x, pos_y, width, height, color, _id) {
        super();

    this.id = _id;
    this.draw_self(width, height, color);
    this.interactive = true;
    this.x = pos_x;
    this.y = pos_y;
    this.on('mousedown', pointer_down);
    this.on('mouseover', pointer_over);
    this.on('mouseup', pointer_up);
        }


    draw_self(width, height, color) {
        this.lineStyle(1, 0x0);
    this.beginFill(color);
    this.drawRect(0, 0, width, height);
    this.endFill();
            //this.on('mousedown', pointer_down);
            //this.on('mouseover', pointer_over);
            //this.on('mouseup', pointer_up);
        }
    update_pos(pos_x, pos_y) {
        this.x = pos_x;
    this.y = pos_y;
        }


    }

    function pointer_over() {
        //console.log(`I am square ${this.id}`);
        if (this.id == 2 && mouse_down) {
        this.clear();
    this.beginFill(0);
    this.drawRect(0, 0, b_width, b_height);
        }
    }
    function pointer_up() {
        mouse_down = false;
    }
    function pointer_down() {
        this.clear();
    var color = 0x0;
    this.beginFill(color);
    this.drawRect(0, 0, b_width, b_height);
    mouse_down = true;

    var id = this.id;



    var URL = "/TimeSchedules/SetSchedule";
    var DATA = {id
    };
    $.post(URL, DATA)
    .done(function (result) {
        console.log("send the schedule");
            })
    .fail(function (result) {
        console.log("you didnt send schedule");
            })
    .always(function (result) {

    });
        //console.log("1");
    }

    function updateBoxColor(t1, s_height, delt)
    {

        app.stage.addChild(new Box(4 + t1 * b_width + t1 * border, 50 + s_height * b_height, b_width, delt * b_height, 0x0, id = 0));

    }

    function readSchedule(s) {

        if (!s.isOpen)
    return;

    var s_hours = s.start_hour;
    var s_min = s.start_min;


    var e_hour = s.end_hour;
    var e_min = s.end_min;
    var t1 = -1;
    var weekdays = s.weekd;
    if (weekdays == 'Monday') {
        t1 = 0;
        }
    else if (weekdays == 'Tuesday') {
        t1 = 1;
        }
    else if (weekdays == 'Wednesday') {
        t1 = 2;
        }
    else if (weekdays == 'Thursday') {
        t1 = 3;
        }
    else if (weekdays == 'Friday') {
        t1 = 4;
        }

    var s_hour_height = (s_hours - 8) * 4;
    var e_hour_height = (e_hour - 8) * 4;

    var s_min_height = s_min / 15;
    var e_min_height = e_min / 15;

    var s_height = s_hour_height + s_min_height;
    var e_height = e_hour_height + e_min_height;
    var delt = e_height - s_height;

    updateBoxColor(t1, s_height, delt);
    }

    $(function () {
        setup_pixi_stage(600, 1600)
    //set weekdays text
    var mon = new PIXI.Text('MO', {fontSize: '25px', fill: 'white', align: 'left' }); mon.position.x = 25; mon.position.y = 10;
    var tu = new PIXI.Text('TU', {fontSize: '25px', fill: 'white', align: 'left' }); tu.position.x = 25 + b_width + border; tu.position.y = 10;
    var we = new PIXI.Text('WE', {fontSize: '25px', fill: 'white', align: 'left' }); we.position.x = 25 + 2 * b_width + 2 * border; we.position.y = 10;
    var th = new PIXI.Text('WE', {fontSize: '25px', fill: 'white', align: 'left' }); th.position.x = 25 + 3 * b_width + 3 * border; th.position.y = 10;
    var fr = new PIXI.Text('FR', {fontSize: '25px', fill: 'white', align: 'left' }); fr.position.x = 25 + 4 * b_width + 4 * border; fr.position.y = 10;

    var n = 0;
    //set time text
    var am8 = new PIXI.Text('8:00', {fontSize: '20px', fill: 'white', align: 'left' }); am8.position.x = 125 + 4 * b_width + 4 * border; am8.position.y = 40 + n * 4 * b_height; n += 1;
    var am9 = new PIXI.Text('9:00', {fontSize: '20px', fill: 'white', align: 'left' }); am9.position.x = 125 + 4 * b_width + 4 * border; am9.position.y = 40 + n * 4 * b_height; n += 1;
    var am10 = new PIXI.Text('10:00', {fontSize: '20px', fill: 'white', align: 'left' }); am10.position.x = 125 + 4 * b_width + 4 * border; am10.position.y = 40 + n * 4 * b_height; n += 1;
    var am11 = new PIXI.Text('11:00', {fontSize: '20px', fill: 'white', align: 'left' }); am11.position.x = 125 + 4 * b_width + 4 * border; am11.position.y = 40 + n * 4 * b_height; n += 1;
    var am12 = new PIXI.Text('12:00', {fontSize: '20px', fill: 'white', align: 'left' }); am12.position.x = 125 + 4 * b_width + 4 * border; am12.position.y = 40 + n * 4 * b_height; n += 1;
    var am13 = new PIXI.Text('13:00', {fontSize: '20px', fill: 'white', align: 'left' }); am13.position.x = 125 + 4 * b_width + 4 * border; am13.position.y = 40 + n * 4 * b_height; n += 1;
    var am14 = new PIXI.Text('14:00', {fontSize: '20px', fill: 'white', align: 'left' }); am14.position.x = 125 + 4 * b_width + 4 * border; am14.position.y = 40 + n * 4 * b_height; n += 1;
    var am15 = new PIXI.Text('15:00', {fontSize: '20px', fill: 'white', align: 'left' }); am15.position.x = 125 + 4 * b_width + 4 * border; am15.position.y = 40 + n * 4 * b_height; n += 1;
    var am16 = new PIXI.Text('16:00', {fontSize: '20px', fill: 'white', align: 'left' }); am16.position.x = 125 + 4 * b_width + 4 * border; am16.position.y = 40 + n * 4 * b_height; n += 1;
    var am17 = new PIXI.Text('17:00', {fontSize: '20px', fill: 'white', align: 'left' }); am17.position.x = 125 + 4 * b_width + 4 * border; am17.position.y = 40 + n * 4 * b_height; n += 1;
    var am18 = new PIXI.Text('18:00', {fontSize: '20px', fill: 'white', align: 'left' }); am18.position.x = 125 + 4 * b_width + 4 * border; am18.position.y = 40 + n * 4 * b_height; n += 1;
    var am19 = new PIXI.Text('19:00', {fontSize: '20px', fill: 'white', align: 'left' }); am19.position.x = 125 + 4 * b_width + 4 * border; am19.position.y = 40 + n * 4 * b_height; n += 1;
    var am20 = new PIXI.Text('20:00', {fontSize: '20px', fill: 'white', align: 'left' }); am20.position.x = 125 + 4 * b_width + 4 * border; am20.position.y = 40 + n * 4 * b_height; n += 1;



    //adding weekdays
    app.stage.addChild(mon);
    app.stage.addChild(tu);
    app.stage.addChild(we);
    app.stage.addChild(th);
    app.stage.addChild(fr);


    app.stage.addChild(am8);
    app.stage.addChild(am9);
    app.stage.addChild(am10);
    app.stage.addChild(am11);
    app.stage.addChild(am12);
    app.stage.addChild(am13);
    app.stage.addChild(am14);
    app.stage.addChild(am15);
    app.stage.addChild(am16);
    app.stage.addChild(am17);
    app.stage.addChild(am18);
    app.stage.addChild(am19);
    app.stage.addChild(am20);


    //set up columns

    //var a = new Box(4 + b_width + border, 50, b_width, b_height, 0xFFFFFF, id= n )
    //app.stage.addChild(a); n++;
    ////a.on('mousedown', pointer_down);
    ////a.on('mouseover', pointer_over);
    ////a.on('mouseup', pointer_up);
    //app.stage.addChild(new Box(4 + 2 * b_width + 2 * border, 50, b_width, b_height, 0xFFFFFF, id = n)); n++;
    //app.stage.addChild(new Box(4 + 3 * b_width + 3 * border, 50, b_width, b_height, 0xFFFFFF, id = n)); n++;
    //app.stage.addChild(new Box(4 + 4 * b_width + 4 * border, 50, b_width, b_height, 0xFFFFFF, id = n)); n++;

    var i = 0;
    var j = 0;
    n = 0;
    for (i; i < 12 * 4; i++) {
        // var a = new Box(4 + j * b_width + j * border, 50 + i * b_height, b_width, b_height, 0xFFFFFF, id = n);
        app.stage.addChild(new Box(4 + j * b_width + j * border, 50 + i * b_height, b_width, b_height, 0xFFFFFF, id = n)); n++;
    app.stage.addChild(new Box(4 + (j + 1) * b_width + (j + 1) * border, 50 + i * b_height, b_width, b_height, 0xFFFFFF, id = n)); n++;
    app.stage.addChild(new Box(4 + (j + 2) * b_width + (j + 2) * border, 50 + i * b_height, b_width, b_height, 0xFFFFFF, id = n)); n++;
    app.stage.addChild(new Box(4 + (j + 3) * b_width + (j + 3) * border, 50 + i * b_height, b_width, b_height, 0xFFFFFF, id = n)); n++;
    app.stage.addChild(new Box(4 + (j + 4) * b_width + (j + 4) * border, 50 + i * b_height, b_width, b_height, 0xFFFFFF, id = n)); n++;
    }
    var result;
    var URL = "/TimeSchedules/GetSchedule";
    var DATA = {
    };
    $.post(URL, DATA)
    .done(function (result) {
        console.log("get the schedule");
    for (let k = 0; k < result.length; k++) {
        readSchedule(result[k]);
                }
            })
    .fail(function (result) {
        console.log("you didnt get schedule");
            })
    .always(function (result) {

    });




    //app.stage.addChild(new Box(4 + 2 * b_width + 2 * border, 50 + 29 * b_height, b_width, 5 * b_height, 0x0, id = 0));

        //updateData(2, 29, 5);




    });


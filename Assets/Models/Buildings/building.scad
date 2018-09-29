//Building Settings
width = 22;
depth = 22;
height = 50;
stories = 8;

//Window Settings
windowheight = 5;
windowwidth = 5;
windowdepth = 0.5;
windowspacefrombottom = 0;
windowspacefromtop = 0;
numberofwindows = 2;

//Door Settings
dooronsides = [1,2];
doorwidth = 1.5;
doordepth = 1;
doorheight = 3;

difference(){
translate([0,0,height/2]) cube([width,depth,height],center=true);

for(r=[0:90:359]){
  rotate([0,0,r]){
    for(door=dooronsides){
      if(r/90 == door){
        translate([width/2,0,doorheight/2])
          cube([doorwidth,doordepth+1,doorheight+1],center=true);
        }
      }
    for(s=[1:1:stories])
      for(n=[0:1:numberofwindows -1]){
        translate([width/2,(width-windowwidth*numberofwindows)/(numberofwindows+1) * (n + 1) + (n + 1/2) * windowwidth - width/2,(height-windowspacefrombottom-windowspacefromtop-doorheight)/(stories+1)*(s)+windowspacefrombottom+doorheight])
        cube([windowdepth,windowwidth,windowheight],center=true);
        
      }
    }
  }
}
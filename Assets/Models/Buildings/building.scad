//Building Settings
width = 22;
depth = 22;
height = 60;
stories = 8;

//Window Settings
windowheight = 5;
windowwidth = 5;
windowdepth = 0.5;
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
    for(s=[0:1:stories - 1])
      for(n=[0:1:numberofwindows -1]){
        translate([width/2,CalculateEvenSpacing(totalWidth = width, itemWidth = windowwidth, itemCount = numberofwindows, itemIndex = n), CalculateEvenSpacing(totalWidth = height - doorheight, itemWidth = windowheight, itemCount = stories, itemIndex = s) + doorheight/2 + height/2])
        cube([windowdepth,windowwidth,windowheight],center=true);
        
      }
    }
  }
}

function CalculateEvenSpacing(totalWidth, itemWidth, itemCount, itemIndex) =
        (totalWidth-itemWidth*itemCount)/(itemCount+1) * (itemIndex + 1) + (itemIndex + 1/2) * itemWidth - totalWidth/2;
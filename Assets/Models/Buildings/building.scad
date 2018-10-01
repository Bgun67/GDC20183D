//Building Settings
buildingwidth = 22;
buildingdepth = 40;
height = 60;
stories = 8;

//Window Settings
windowheight = 5;
windowwidth = 5;
windowdepth = 0.5;
numberofwindows = 2;
windowonsides = [1,1,0,0];

//Door Settings
dooronsides = [1,2];
doorwidth = 1.5;
doordepth = 1;
doorheight = 3;

difference(){
translate([0,0,height/2]) cube([buildingwidth,buildingdepth,height],center=true);

for(r=[0:90:359]){
  rotate([0,0,r]){
    for(door=dooronsides){
      if(r/90 == door){
        translate([width(r)/2,0,doorheight/2])
          cube([doorwidth,doordepth+1,doorheight+1],center=true);
        }
      }
      if(windowonsides[r/90] == 1)
        for(s=[0:1:stories - 1])
          for(n=[0:1:numberofwindows -1]){
            translate([width(r)/2,CalculateEvenSpacing(totalWidth = width(r-90), itemWidth = windowwidth, itemCount = numberofwindows, itemIndex = n), CalculateEvenSpacing(totalWidth = height - doorheight, itemWidth = windowheight, itemCount = stories, itemIndex = s) + doorheight/2 + height/2])
            cube([windowdepth,windowwidth,windowheight],center=true);
          }
    }
  }
}

function CalculateEvenSpacing(totalWidth, itemWidth, itemCount, itemIndex) =
        (totalWidth-itemWidth*itemCount)/(itemCount+1) * (itemIndex + 1) + (itemIndex + 1/2) * itemWidth - totalWidth/2;

function width(r) = (r == 0 || r == 180) ? buildingwidth : buildingdepth;
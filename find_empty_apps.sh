#!/bin/bash
cd /home/user/Apps
for dir in */; do
  appName="${dir%/}"
  kebab=$(echo "$appName" | sed 's/\([a-z]\)\([A-Z]\)/\1-\2/g' | tr 'A-Z' 'a-z')
  path="$appName/src/$appName.WebApp/projects/$kebab/src/app"
  if [ -d "$path" ] && [ ! -d "$path/models" ]; then
    admin="$appName/src/$appName.WebApp/projects/${kebab}-admin"
    if [ ! -d "$admin" ]; then
      pcount=$(ls -d "$appName/src/$appName.WebApp/projects"/*/ 2>/dev/null | wc -l)
      if [ "$pcount" -le 1 ]; then
        echo "$appName"
      fi
    fi
  fi
done

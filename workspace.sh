#https://app.pluralsight.com/course-player?clipId=2e4e2609-f045-4c73-8601-04d6d6482135
#!/bin/bash

echo "*********** Create or select workspace"
if [ $(terraform workspace list | grep -c "$1") -eq 0 ] ; then
  echo "Create new workspace $1"
  terraform workspace new "$1" -no-color
else
  echo "Switch to workspace $1"
  terraform workspace select "$1" -no-color
fi
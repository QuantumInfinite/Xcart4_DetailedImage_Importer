# Xcart4_DetailedImage_Importer
Creates a CSV file for your detailed images for xcart 4.x websites

In order for this to work properly, you must create a local image file system wherein every folder is the productcode (SKU) 
of a product on your website. The product codes must match exactly for this to work. Them on the server hosting your website, 
you must create a duplicate file system (this is where your website will actually pull the images from)

1. Place your images in the folders (jpg's only), and run the exe located in bin\Release\netcoreapp2.0\win10-x64.
2. The exe will ask you for your local files (copy and paste the url from file explorer, ie C:\Users\You\Desktop\images)
3. Next it will ask for the place on the server where your files are located (ie. /var/www/vhosts/[yourwebsite].com/httpdocs/new/files/userfiles_1/images).
3. Lastly it will ask for where you want the csv file to be created. it is fine to leave this field blank.
4. After it finishes computing, you can open the csv and verify that it looks right and then head over to [yourwebsite].com/admin/import.php?,
load up the CSV, and hit import.
5. After that, regenerate the image cashe at [yourwebsite].com/new/admin/tools.php.

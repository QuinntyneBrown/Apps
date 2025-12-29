const { chromium } = require('playwright');
const path = require('path');
const fs = require('fs');

async function takeScreenshot(htmlPath, outputPath) {
    const browser = await chromium.launch({ headless: true });
    const page = await browser.newPage();
    await page.setViewportSize({ width: 1440, height: 900 });

    const absolutePath = path.resolve(htmlPath);
    await page.goto(`file://${absolutePath}`, { waitUntil: 'networkidle' });

    await page.screenshot({ path: outputPath, fullPage: false });
    await browser.close();
    console.log(`Screenshot saved: ${outputPath}`);
}

async function processDirectory(dir) {
    const files = fs.readdirSync(dir);
    for (const file of files) {
        if (file.endsWith('.html')) {
            const htmlPath = path.join(dir, file);
            const pngPath = htmlPath.replace('.html', '.png');
            try {
                await takeScreenshot(htmlPath, pngPath);
            } catch (e) {
                console.error(`Error processing ${htmlPath}: ${e.message}`);
            }
        }
    }
}

// Process command line args
const args = process.argv.slice(2);
if (args.length === 0) {
    console.log('Usage: node screenshot.js <html-file-or-directory>');
    process.exit(1);
}

const target = args[0];
if (fs.statSync(target).isDirectory()) {
    processDirectory(target).catch(console.error);
} else {
    const outputPath = target.replace('.html', '.png');
    takeScreenshot(target, outputPath).catch(console.error);
}

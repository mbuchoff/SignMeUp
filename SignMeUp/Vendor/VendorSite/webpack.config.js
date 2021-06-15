const path = require('path');
const CopyPlugin = require("copy-webpack-plugin");

module.exports = {
    // https://webpack.js.org/configuration/other-options/#cachetype
    cache: { type: 'filesystem' },

    entry: {
        'site': './wwwroot/ts/site.tsx',
    },
    devtool: 'inline-source-map',
    output: {
        path: path.resolve(__dirname, './wwwroot/generated'),
        filename: '[name].bundle.js'
    },
    module: {
        rules: [
            // https://webpack.js.org/guides/typescript/
            {
                test: /\.tsx?$/,
                use: [{
                    loader: 'ts-loader',
                    options: {
                        configFile: 'tsconfig.json'
                    }
                }],
                exclude: /node_modules/,
            },

            // https://webpack.js.org/loaders/sass-loader/
            {
                test: /\.s[ac]ss$/i,
                use: [
                    // Creates `style` nodes from JS strings
                    "style-loader",
                    // Translates CSS into CommonJS
                    "css-loader",
                    // Compiles Sass to CSS
                    "sass-loader",
                ],
            },

            // https://webpack.js.org/loaders/css-loader/
            {
                test: /\.css$/i,
                use: ["style-loader", "css-loader"],
            },
        ],
    },

    // https://webpack.js.org/concepts/module-resolution/
    resolve: {
        extensions: ['.tsx', '.ts', '.js'],
    },

    plugins: [
        // https://webpack.js.org/plugins/copy-webpack-plugin/
        new CopyPlugin({
            patterns: [
                { from: path.resolve(__dirname, './node_modules/jquery/dist/jquery.min.js'), to: '../libs' },
                { from: path.resolve(__dirname, './node_modules/jquery/dist/jquery.js'), to: '../libs' },
                { from: path.resolve(__dirname, './node_modules/bootstrap/dist/css/bootstrap.min.css'), to: '../libs' },
                { from: path.resolve(__dirname, './node_modules/bootstrap/dist/js/bootstrap.min.js'), to: '../libs' },
                { from: path.resolve(__dirname, './node_modules/jquery-ui-dist/jquery-ui.min.js'), to: '../libs' },
                { from: path.resolve(__dirname, './node_modules/underscore/underscore-min.js'), to: '../libs' },
                { from: path.resolve(__dirname, './node_modules/jquery-validation/dist/jquery.validate.min.js'), to: '../libs' },
                { from: path.resolve(__dirname, './node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.min.js'), to: '../libs' },
            ]
        })],
};
const dotenv = require("dotenv");
const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const webpack = require("webpack");
const envPath = path.join(__dirname, ".env.development");
const envConfig = dotenv.config({ path: envPath }).parsed;

module.exports = {
	mode: process.env.NODE_ENV === "production" ? "production" : "development",
	devtool: process.env.NODE_ENV === "development" ? "source-map" : false,
	entry: {
		app: "./src/ts/index.tsx",
	},
	output: {
		filename: "[name].js",
		path: path.resolve(__dirname, "./dist"),
	},
	resolve: {
		extensions: [".ts", ".tsx", ".js", ".scss"],
	},
	module: {
		rules: [
			{
				test: /\.tsx?$/,
				use: "ts-loader",
				exclude: /node_modules/,
			},
			{
				test: /\.s[ac]ss$/i,
				use: [MiniCssExtractPlugin.loader, "css-loader", "sass-loader"],
			},
			{
				test: /\.html$/,
				use: "html-loader",
			},
			{
				test: /\.(png|jpe?g|gif|svg|webp)$/i,
				type: "asset/resource",
				generator: {
					filename: "images/[name][ext]", // This will keep the original name and extension of the file
				},
			},
		],
	},
	plugins: [
		new HtmlWebpackPlugin({
			template: "./public/index.html",
			filename: "index.html",
			inject: true, // This ensures the plugin will inject the scripts and links for CSS
		}),
		new MiniCssExtractPlugin({
			filename: "[name].css", // This will produce app.css, but you don't need to worry about it since it will be injected
		}),
		new webpack.DefinePlugin({
			"process.env": JSON.stringify({
				REACT_APP_API_BASE_URL: envConfig.REACT_APP_API_BASE_URL,
				REACT_APP_IMAGE_PATH: envConfig.REACT_APP_IMAGE_PATH,
				// ... any other env vars you need to define
			}),
		}),
	],
	devServer: {
		static: {
			directory: path.join(__dirname, "./dist"),
		},
		hot: true,
		historyApiFallback: true, // Fallback for single-page applications
		client: {
			webSocketURL: "ws://0.0.0.0:3000/ws", // Adjust port if necessary
		},
		headers: { "Access-Control-Allow-Origin": "*" },
		open: true,
		compress: true,
		port: 3000,
		watchFiles: {
			paths: ["src/**/*", "public/**/*"], // Watch these paths for changes
			options: {
				usePolling: true,
				poll: 1000, // Check for changes every second
			},
		},
	},
};

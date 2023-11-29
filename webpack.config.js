const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = {
	mode: process.env.NODE_ENV === "production" ? "production" : "development",
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
				test: /\.(png|jpe?g|gif|svg)$/i,
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
	],
	devServer: {
		static: {
			directory: path.join(__dirname, "./dist"),
		},
		open: true,
		compress: true,
		liveReload: true,
		port: 3000,
	},
	devtool: process.env.NODE_ENV === "development" ? "source-map" : false,
};

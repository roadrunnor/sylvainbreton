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
		app: "./src/index.tsx",
	},
	output: {
		filename: "[name].js",
		path: path.resolve(__dirname, "./dist"),
		publicPath: "/",
	},
	resolve: {
		extensions: [".ts", ".tsx", ".js", ".svg", ".scss"],
		alias: {
			assets: path.resolve(__dirname, "public/assets"),
		},
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
				test: /\.svg$/,
				issuer: /\.[jt]sx?$/,
				use: [
					{
						loader: "@svgr/webpack",
						options: {
							defaultExport: "ReactComponent",
						},
					},
					"file-loader",
				],
			},
			{
				test: /\.(png|jpe?g|gif|webp)$/i,
				type: "asset/resource",
				generator: {
					filename: "images/[name][ext]",
				},
			},
		],
	},
	plugins: [
		new HtmlWebpackPlugin({
			template: "./public/index.html",
			filename: "index.html",
			inject: true,
		}),
		new MiniCssExtractPlugin({
			filename: "[name].css",
		}),
		new webpack.DefinePlugin({
			"process.env": JSON.stringify({
				REACT_APP_API_BASE_URL: envConfig.REACT_APP_API_BASE_URL,
				REACT_APP_IMAGE_PATH: envConfig.REACT_APP_IMAGE_PATH,
			}),
		}),
	],
	devServer: {
		static: {
			directory: path.join(__dirname, "public"),
		},
		hot: true,
		historyApiFallback: true,
		client: {
			webSocketURL: "ws://0.0.0.0:3000/ws",
		},
		headers: { "Access-Control-Allow-Origin": "*" },
		open: true,
		compress: true,
		port: 3000,
		watchFiles: {
			paths: ["src/**/*", "public/**/*"],
			options: {
				usePolling: true,
				poll: 5000,
			},
		},
	},
};

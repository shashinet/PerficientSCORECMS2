console.log('\r\n ClientResources webpack')
const MiniCssExtractPlugin = require('mini-css-extract-plugin')
const path = require('path')

module.exports = (args) => {
	// unify mode settings
	if (!process.env.NODE_ENV && args.mode) {
		process.env.NODE_ENV = args.mode
	} else if (!args.mode && process.env.NODE_ENV) {
		args.mode = process.env.NODE_ENV
	} else {
		console.warn('No mode env variable set!')
	}
	const entryPoint = `./entry.js`
	const outputDir = path.resolve(`../dev/src/Web/wwwroot/ClientResources/dist`)
	console.log(`Mode: ${args.mode}`)
	console.log(`Entry: ${entryPoint}`)
	console.log(`Output: ${outputDir}`)

	return {
		entry: entryPoint,
		output: {
			path: outputDir,
		},
		resolve: {
			extensions: ['*', '.js', '.jsx', '.scss'],
		},
		module: {
			rules: [
				{
					test: /\.(js|jsx)$/,
					exclude: /node_modules/,
					use: ['babel-loader', 'eslint-loader'],
				},
				{
					test: /\.s?css/,
					use: [MiniCssExtractPlugin.loader, 'css-loader', 'sass-loader'],
				},
			],
		},
		plugins: [
			new MiniCssExtractPlugin({
				filename: './styles.css',
			}),
		],
	}
}

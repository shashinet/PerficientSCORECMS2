import React from 'react'

export default {
	title: 'Blocks/Blog',
}

export function Full() {
	return (
		<div className="score-blog-post">			
			<h2>Blog Post Title</h2>
			<p className="date">
				Posted on <em>June 8th</em> to{' '}
				<a href="http://test.com">
					<em>Category Name</em>
				</a>{' '}
				by{' '}
				<a href="/">
					<em>Author Name</em>
				</a>
			</p>
			<div className="rich-text">
				<p>
					Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi sit amet eleifend felis. Phasellus
					vitae mauris rhoncus, finibus felis non, rutrum magna. Lorem ipsum dolor sit amet, consectetur
					adipiscing elit. Morbi sit amet eleifend felis. Phasellus vitae mauris rhoncus, finibus felis non,
					rutrum magna.
				</p>
				<blockquote className="fancy-quotes">
					Morbi sit amet eleifend felis. Phasellus vitae mauris rhoncus, finibus felis non, rutrum magna.
				</blockquote>
				<p>
					Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi sit amet eleifend felis. Phasellus
					vitae mauris rhoncus, finibus felis non, rutrum magna. Lorem ipsum dolor sit amet, consectetur
					adipiscing elit. Morbi sit amet eleifend felis. Phasellus vitae mauris rhoncus, finibus felis non,
					rutrum magna.
				</p>
				<p>
					Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi sit amet eleifend felis. Phasellus
					vitae mauris rhoncus, finibus felis non, rutrum magna. Lorem ipsum dolor sit amet, consectetur
					adipiscing elit. Morbi sit amet eleifend felis. Phasellus vitae mauris rhoncus, finibus felis non,
					rutrum magna.
				</p>
				<img
					src="https://picsum.photos/600/600"
					alt="some thing to see here"
					className="score-image img-fluid"
				/>

				<p>
					Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi sit amet eleifend felis. Phasellus
					vitae mauris rhoncus, finibus felis non, rutrum magna. Lorem ipsum dolor sit amet, consectetur
					adipiscing elit. Morbi sit amet eleifend felis. Phasellus vitae mauris rhoncus, finibus felis non,
					rutrum magna.
				</p>
				<p>
					Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi sit amet eleifend felis. Phasellus
					vitae mauris rhoncus, finibus felis non, rutrum magna. Lorem ipsum dolor sit amet, consectetur
					adipiscing elit. Morbi sit amet eleifend felis. Phasellus vitae mauris rhoncus, finibus felis non,
					rutrum magna.
				</p>
				<p>
					Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi sit amet eleifend felis. Phasellus
					vitae mauris rhoncus, finibus felis non, rutrum magna. Lorem ipsum dolor sit amet, consectetur
					adipiscing elit. Morbi sit amet eleifend felis. Phasellus vitae mauris rhoncus, finibus felis non,
					rutrum magna.
				</p>
			</div>
		</div>
	)
}

export function Summary() {
	return (
		<div className="score-blog-post score-blog-post-summary">
			<a href="/">
				<img
					src="https://picsum.photos/800/400"
					alt="some thing to see here"
					className="score-image img-fluid"
				/>
			</a>
			<h2>
				<a href="/">Blog Post Title</a>
			</h2>
			<p className="date">
				Posted on <em>June 8th</em> to{' '}
				<a href="/">
					<em>Category Name</em>
				</a>{' '}
				by{' '}
				<a href="/">
					<em>Author Name</em>
				</a>
			</p>
			<div className="rich-text">
				Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi sit amet eleifend felis. Phasellus vitae
				mauris rhoncus, finibus felis non, rutrum magna. Lorem ipsum dolor sit amet, consectetur adipiscing
				elit. Morbi sit amet eleifend felis. Phasellus vitae mauris rhoncus, finibus felis non, rutrum magna.
			</div>
			<a href="/" className="score-button text-primary md" aria-label="Read More">
				Read More
			</a>
		</div>
	)
}

export function Card() {
	return (
		<>
			<div className="score-blog-post score-blog-post-card w-4col">
				<div className="score-card flush-image">
					<div className="image-wrapper">
						<img
							src="https://picsum.photos/400"
							alt="some thing to see here"
							className="score-image img-fluid"
						/>
					</div>
					<div className="caption">
						<h2>This is a long heading for a blog post</h2>
						<div className="score-card-body">
							<div className="rich-text">
								Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi sit amet eleifend felis.
								Phasellus vitae mauris rhoncus, finibus felis non, rutrum magna.
							</div>
						</div>
						<div className="score-call-to-action">
							<a href="/" className="score-button primary" aria-label="Read More">
								Read More
							</a>
						</div>
					</div>
				</div>
			</div>
		</>
	)
}

export function Hero() {
	return (
		<>
			<div className="score-blog-post score-blog-post-hero w-full">
				<div className="w-5col image-wrapper">
					<img src="https://picsum.photos/400" alt="some thing to see here" className="img-fluid" />
				</div>
				<div className="w-7col caption">
					<h6>
						<a href="/url/of/blog">Title of blog</a> | <a href="/">Category of blog post</a>
					</h6>
					<h2>This is a long heading for a blog hero</h2>
					<p className="date">
						<em>June 8th</em>
					</p>
					<div className="rich-text">
						Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi sit amet eleifend felis.
						Phasellus vitae mauris rhoncus, finibus felis non, rutrum magna.
					</div>
					<a href="/" className="score-button primary" aria-label="Read More">
						Read More
					</a>
				</div>
			</div>
		</>
	)
}

export function Mini(wrapper) {
	let template = (
		<div className="score-blog-post score-blog-post-mini">
			<div className="w-5col image-wrapper">
				<a href="/">
					<img
						src="https://picsum.photos/200/200"
						alt="some thing to see here"
						className="score-image img-fluid"
					/>
				</a>
			</div>
			<div className="w-7col caption">
				<h3>
					<a href="/">Category name</a>
				</h3>
				<h2>
					<a href="/">Blog Post Title</a>
				</h2>
			</div>
		</div>
	)
	if (wrapper) {
		template = <div className="w-5col">{template}</div>
	}
	return <>{template}</>
}

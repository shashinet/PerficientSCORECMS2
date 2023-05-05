import React from 'react'

export default {
	title: 'Blocks/Blog',
}

export function Header() {
	return (
		<div className="score-blog-header">
			<section className="score-stripe score-picture-stripe default">
				<picture>
					<source
						media="(min-width: 1200px)"
						srcSet="https://picsum.photos/id/2/1400/300?grayscale"
						type="image/jpeg"
					/>
					<source
						media="(min-width: 767px)"
						srcSet="https://picsum.photos/id/2/1000/300?grayscale"
						type="image/jpeg"
					/>
					<source
						media="(min-width: 600px)"
						srcSet="https://picsum.photos/id/2/400/200?grayscale"
						type="image/jpeg"
					/>
					<img src="https://picsum.photos/id/2/100/300?grayscale" alt="alt text area" />
				</picture>

				<div className="section-header">
					<h1>Blog Title Here</h1>
					<h2>Blog Category Title</h2>
				</div>
			</section>
		</div>
	)
}

import React from 'react'
import { Header } from '../../Blocks/Blog/parts.stories'
import { Summary } from '../../Blocks/Blog/post.stories'

export default {
	title: 'Layouts/Blog/Archive',
	parameters: {
		// layout: 'fullscreen',
	},
}

export function Archive() {
	return (
		<>
			{Header()}

			<section className="container">
				<div className="w-8col score-blog-archive score-blog-archive-posts">
					<h5>Showing 13 posts in "Category Name"</h5>
					{Summary()}
					{Summary()}
					{Summary()}
				</div>

				<div className="w-4col score-blog-sidebar">
					<h4>Categories</h4>
					<ul className="categories">
						{CategoryLink()}
						{CategoryLink('Really really long category name goes riiiiight here')}
						{CategoryLink()}
						{CategoryLink('Short')}
						{CategoryLink()}
					</ul>
					<hr />
					<h4>Archives</h4>
					<ul className="archives">
						{ArchiveLink('November 2022')}
						{ArchiveLink('October 2022')}
						{ArchiveLink('September 2022')}
						{ArchiveLink('August 2022')}
						{ArchiveLink('July 2022')}
					</ul>
					<hr />
				</div>
			</section>
		</>
	)
}

function CategoryLink(name = 'Category Name') {
	return (
		<li>
			<a href="/">{name}</a> <span className="counter">[{Math.ceil(Math.random() * 10)}]</span>
		</li>
	)
}

function ArchiveLink(name = 'Archive Name') {
	return (
		<li>
			<a href="/">{name}</a> <span className="counter">[{Math.ceil(Math.random() * 10)}]</span>
		</li>
	)
}

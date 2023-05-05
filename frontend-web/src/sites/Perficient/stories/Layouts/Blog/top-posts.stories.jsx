import React from 'react'
import { Card, Mini } from '../../Blocks/Blog/post.stories'

export default {
	title: 'Layouts/Blog/TopPosts',
	parameters: {
		// layout: 'fullscreen',
	},
}

export function TopPosts() {
	return (
		<>
			<section className="container">
				<div className="w-4col score-blog-top-posts">
					{Mini()}
					{Mini()}
					{Mini()}
					{Mini()}
				</div>

				{Card()}
				{Card()}
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

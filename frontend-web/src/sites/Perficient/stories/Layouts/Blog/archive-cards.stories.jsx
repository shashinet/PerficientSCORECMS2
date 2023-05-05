import React from 'react'
import { Header } from '../../Blocks/Blog/parts.stories'
import { Card } from '../../Blocks/Blog/post.stories'

export default {
	title: 'Layouts/Blog/ArchiveCards',
	parameters: {
		// layout: 'fullscreen',
	},
}

export function ArchiveCards() {
	return (
		<>
			{Header()}

			<section className="container score-blog-archive score-blog-archive-cards">
				<ul className="categories w-full">
					{CategoryLink()}
					{CategoryLink('Active category', true)}
					{CategoryLink('Long category name here')}
					{CategoryLink('Short')}
					{CategoryLink()}
				</ul>

				{Card()}
				{Card()}
				{Card()}
				{Card()}
				{Card()}
				{Card()}
			</section>
		</>
	)
}

function CategoryLink(name = 'Category Name', active = false) {
	return (
		<li className={active ? 'active' : ''}>
			<a href="/">
				{name} <span className="counter">[{Math.ceil(Math.random() * 10)}]</span>
			</a>
		</li>
	)
}

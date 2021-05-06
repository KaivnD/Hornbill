#!/usr/bin/env node
'use strict'
require('dotenv').config()
const program = require('commander')

const { version } = require('../package')
const isCI = process.env.CI || false
if (isCI) throw new Error('Hornbill is not support CI env')
process.noDeprecation = true

;(async () => {
  program
    .name('hornbill')
    .description('Hornbill Command Line Interface')
    .version(version)
    .usage('[command] [script-filename]')

  program.command('hello').action(() => console.log('hi! hornbill'))

  program
    .command('run <entry>')
    .action((entry, options) => require('./run')(entry, options))

  program.parse(process.argv)
})()

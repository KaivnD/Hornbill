const path = require('path')
const fs = require('fs')
const WebSocket = require('ws')
const pwd = process.cwd()

module.exports = async function (entry, options) {
  if (!entry || typeof entry !== 'string') throw new Error('please name your entry')
  if (!entry.endsWith('.js')) entry += '/index.js'

  const filename = path.resolve(pwd, entry)
  if (!fs.existsSync(filename)) throw new Error(`no such file named ${filename}`)

  try {
    const ws = new WebSocket('ws://127.0.0.1:9393')
    ws.on('open', () => {
      ws.send(filename)
      ws.terminate()
    })
  } catch {
    throw new Error('run failed')
  }
}